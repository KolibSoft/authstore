import { CredentialModel } from "./models/credential_model.js";
import { CredentialPermissionModel } from "./models/credential_permission_model.js";
import { PermissionModel } from "./models/permission_model.js";
import { CredentialPermissionFilters } from "./credential_permission_filters.js";
import { CatalogueStatics, DatabaseCatalogue, DbContext, DbSet } from "./modules/catalogue.js";

class CredentialPermissionDatabaseCatalogue extends DatabaseCatalogue {

    /** @type {DbSet<CredentialModel>} */
    #credentials;

    /** @type {DbSet<PermissionModel>} */
    #permissions;

    /** @returns {DbSet<CredentialModel>} */
    get credentials() { return this.#credentials; }

    /** @returns {DbSet<PermissionModel>} */
    get permissions() { return this.#permissions; }

    /**
     * @param {CredentialPermissionModel[]} items 
     * @param {CredentialPermissionFilters} filters 
     * @returns {Promise<CredentialPermissionModel[]>}
     */
    async queryItems(items, filters = null) {
        if (filters?.clean) items = items.filter(x => x.active);
        if (filters?.credentialId) items = items.filter(x => x.credentialId == filters.credentialId);
        if (filter?.permissionId) items = items.filter(x => x.permissionId == filters.permissionId);
        items = items.sort((a, b) => b.active - a.active);
        return items;
    }

    /**
     * @param {CredentialPermissionModel} item 
     * @returns {boolean}
     */
    async vlidateInsert(item) {
        if (!(await this.#credentials.filter(x => x.id == item.credentialId)).length > 0) {
            this.errors.push(AuthStoreStatics.InvalidCredential);
            return false;
        }
        if (!(await this.#permissions.filter(x => x.id == item.permissionId)).length > 0) {
            this.errors.push(AuthStoreStatics.InvalidPermission);
            return false;
        }
        if ((await this.dbSet.filter(x => x.credentialId == item.credentialId && x.permissionId == item.permissionId)).length > 0) {
            this.errors.push(CatalogueStatics.RepeatedItem);
            return false;
        }
        return true;
    }

    /**
     * @param {CredentialPermissionModel} item 
     * @returns {boolean}
     */
    async vlidateUpdate(item) {
        if (!(await this.#credentials.filter(x => x.id == item.credentialId)).length > 0) {
            this.errors.push(AuthStoreStatics.InvalidCredential);
            return false;
        }
        if (!(await this.#permissions.filter(x => x.id == item.permissionId)).length > 0) {
            this.errors.push(AuthStoreStatics.InvalidPermission);
            return false;
        }
        if ((await this.dbSet.filter(x => x.credentialId == item.credentialId && x.permissionId == item.permissionId && x.id != item.id)).length > 0) {
            this.errors.push(CatalogueStatics.RepeatedItem);
            return false;
        }
        return true;
    }

    /**
     * @param {(json: {} => CredentialPermissionModel)} creator 
     * @param {DbContext} dbContext 
     * @param {string} name 
     */
    constructor(creator, dbContext, name) {
        super(creator, dbContext, name);
        this.#credentials = dbContext.set("credential");
        this.#permissions = dbContext.set("permission");
    }

}

export {
    CredentialPermissionDatabaseCatalogue
}