import { CredentialModel } from "./main.js";
import { DatabaseCatalogue, DbContext } from "./modules/catalogue.js";

class CredentialDatabaseCatalogue extends DatabaseCatalogue {

    /**
     * @param {(json: {} => CredentialModel)} creator 
     * @param {DbContext} dbContext 
     * @param {string} name 
     */
    constructor(creator, dbContext, name) {
        super(creator, dbContext, name);
    }

}

export {
    CredentialDatabaseCatalogue
}