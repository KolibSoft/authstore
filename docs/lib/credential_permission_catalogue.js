import { CredentialPermissionModel } from "./main.js";
import { DatabaseCatalogue, DbContext } from "./modules/catalogue.js";

class CredentialPermissionDatabaseCatalogue extends DatabaseCatalogue {

    /**
     * @param {(json: {} => CredentialPermissionModel)} creator 
     * @param {DbContext} dbContext 
     * @param {string} name 
     */
    constructor(creator, dbContext, name) {
        super(creator, dbContext, name);
    }

}

export {
    CredentialPermissionDatabaseCatalogue
}