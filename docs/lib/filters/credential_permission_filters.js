import { CatalogueFilters } from "../modules/catalogue.js";

class CredentialPermissionFilters extends CatalogueFilters {

    /** @type {boolean} */
    clean;

    /** @type {string} */
    credentialId;

    /** @type {string} */
    permissionId;

    constructor(json) {
        super(json);
        this.clean = json?.clean ?? true;
        this.credentialId = json?.credentialId ?? null;
        this.permissionId = json?.permissionId ?? null;
    }

}

export {
    CredentialPermissionFilters
}