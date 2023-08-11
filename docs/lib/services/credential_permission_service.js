import { CredentialPermissionModel } from "../models/credential_permission_model.js";
import { CatalogueService } from "../modules/catalogue.js"

class CredentialPermissionService extends CatalogueService {

    /**
     * @param {(json: {}) => CredentialPermissionModel} creator 
     * @param {(input: RequestInfo | URL, init?: RequestInit | undefined) => Promise<Response>} fetch 
     * @param {string} uri 
     */
    constructor(creator, fetch, uri) {
        super(creator, fetch, uri);
    }

}

export {
    CredentialPermissionService
}