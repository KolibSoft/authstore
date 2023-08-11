import { CredentialModel } from "../models/credential_model.js";
import { CatalogueService } from "../modules/catalogue.js"

class CredentialService extends CatalogueService {

    /**
     * @param {(json: {}) => CredentialModel} creator 
     * @param {(input: RequestInfo | URL, init?: RequestInit | undefined) => Promise<Response>} fetch 
     * @param {string} uri 
     */
    constructor(creator, fetch, uri) {
        super(creator, fetch, uri);
    }

}

export {
    CredentialService
}