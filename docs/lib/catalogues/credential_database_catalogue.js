import { AuthStoreStatics } from "../authstore_statics.js";
import { CredentialFilters } from "../filters/credential_filters.js";
import { CredentialModel } from "../models/credential_model.js";
import { CredentialPermissionModel } from "../models/credential_permission_model.js";
import { CatalogueStatics, DatabaseCatalogue, DbContext, DbSet } from "../modules/catalogue.js";

class CredentialDatabaseCatalogue extends DatabaseCatalogue {

    /** @type {DbSet<CredentialPermissionModel>} */
    #credentialPermissions;

    /** @returns {DbSet<CredentialPermissionModel>} */
    get credentialPermissions() { return this.#credentialPermissions; }

    /**
     * @param {CredentialModel[]} items 
     * @param {CredentialFilters} filters 
     * @returns {Promise<CredentialModel[]>}
     */
    async queryItems(items, filters = null) {
        if (filters?.clean) items = items.filter(x => x.active);
        if (filters?.hint) items = items.filter(x => x.identity.includes(filters.hint));
        items = items.sort((a, b) => a.identity.localeCompare(b.identity)).sort((a, b) => b.active - a.active);
        return items;
    }

    /**
     * @param {CredentialModel} item 
     * @returns {boolean}
     */
    async vlidateInsert(item) {
        if ((await this.dbSet.filter(x => x.identity == item.identity)).length > 0) {
            this.errors.push(AuthStoreStatics.RepeatedIdentity);
            return false;
        }
        return true;
    }

    /**
     * @param {CredentialModel} item 
     * @returns {boolean}
     */
    async validateUpdate(item) {
        if ((await this.dbSet.filter(x => x.identity == item.identity && x.id != item.id)).length > 0) {
            this.errors.push(AuthStoreStatics.RepeatedIdentity);
            return false;
        }
        return true;
    }

    /**
     * @param {CredentialModel} item 
     * @returns {boolean}
     */
    async validateDelete(item) {
        if ((await this.#credentialPermissions.filter(x => x.credentialId == item.id)).length > 0) {
            this.errors.push(CatalogueStatics.UsedItem);
            return false;
        }
        return true;
    }

    /**
     * @param {(json: {} => CredentialModel)} creator 
     * @param {DbContext} dbContext 
     * @param {string} name 
     */
    constructor(creator, dbContext, name) {
        super(creator, dbContext, name);
        this.#credentialPermissions = dbContext.set("credential_permission");
    }

}

export {
    CredentialDatabaseCatalogue
}