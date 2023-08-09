import { PermissionModel } from "./main.js";
import { DatabaseCatalogue, DbContext } from "./modules/catalogue.js";

class PermissionDatabaseCatalogue extends DatabaseCatalogue {

    /**
     * @param {(json: {} => PermissionModel)} creator 
     * @param {DbContext} dbContext 
     * @param {string} name 
     */
    constructor(creator, dbContext, name) {
        super(creator, dbContext, name);
    }

}

export {
    PermissionDatabaseCatalogue
}