import { AuthStoreStatics } from "../authstore_statics.js";
import { CredentialPermissionModel } from "../models/credential_permission_model.js";
import { PermissionModel } from "../models/permission_model.js";
import { CatalogueStatics, DatabaseCatalogue, DbContext, DbSet } from "../modules/catalogue.js";
import { PermissionFilters } from "../filters/permission_filters.js";

class PermissionDatabaseCatalogue extends DatabaseCatalogue {

    /** @type {DbSet<CredentialPermissionModel>} */
    #credentialPermissions;

    /** @returns {DbSet<CredentialPermissionModel>} */
    get credentialPermissions() { return this.#credentialPermissions; }

    /**
     * @param {PermissionModel[]} items 
     * @param {PermissionFilters} filters 
     * @returns {Promise<PermissionModel[]>}
     */
    async queryItems(items, filters = null) {
        if (filters?.clean) items = items.filter(x => x.active);
        if (filters?.hint) items = items.filter(x => x.code.includes(filters.hint));
        items = items.sort((a, b) => a.code.localeCompare(b.code)).sort((a, b) => b.active - a.active);
        return items;
    }

    /**
     * @param {PermissionModel} item 
     * @returns {boolean}
     */
    async vlidateInsert(item) {
        if ((await this.dbSet.filter(x => x.code == item.code)).length > 0) {
            this.errors.push(AuthStoreStatics.RepeatedIdentity);
            return false;
        }
        return true;
    }

    /**
     * @param {PermissionModel} item 
     * @returns {boolean}
     */
    async validateUpdate(item) {
        if ((await this.dbSet.filter(x => x.code == item.code && x.id != item.id)).length > 0) {
            this.errors.push(AuthStoreStatics.RepeatedIdentity);
            return false;
        }
        return true;
    }

    /**
     * @param {PermissionModel} item 
     * @returns {boolean}
     */
    async validateDelete(item) {
        if ((await this.#credentialPermissions.filter(x => x.permissionId == item.id)).length > 0) {
            this.errors.push(CatalogueStatics.UsedItem);
            return false;
        }
        return true;
    }

    /**
     * @param {(json: {} => PermissionModel)} creator 
     * @param {DbContext} dbContext 
     * @param {string} name 
     */
    constructor(creator, dbContext, name) {
        super(creator, dbContext, name);
        this.#credentialPermissions = dbContext.set("credential_permission");
    }

}

export {
    PermissionDatabaseCatalogue
}