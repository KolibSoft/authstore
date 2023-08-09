import { CatalogueStatics, Item } from "../modules/catalogue.js";

class CredentialPermissionModel extends Item {

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