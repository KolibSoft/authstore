import { CatalogueStatics } from "../modules/catalogue.js";

class CredentialPermissionModel {

    /** @type {string} */
    credentialId;

    /** @type {string} */
    permissionId;

    /** @type {boolean} */
    active;

    constructor(json) {
        super(json);
        this.credentialId = json?.credentialId ?? CatalogueStatics.NoGuid;
        this.permissionId = json?.permissionId ?? CatalogueStatics.NoGuid;
        this.active = json?.active ?? true;
    }

}

export {
    CredentialPermissionModel
}