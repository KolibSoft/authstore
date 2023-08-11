import { PermissionModel } from "../models/permission_model.js";
import { CatalogueService } from "../modules/catalogue.js"

class PermissionService extends CatalogueService {

    /**
     * @param {(json: {}) => PermissionModel} creator 
     * @param {(input: RequestInfo | URL, init?: RequestInit | undefined) => Promise<Response>} fetch 
     * @param {string} uri 
     */
    constructor(creator, fetch, uri) {
        super(creator, fetch, uri);
    }

}

export {
    PermissionService
}