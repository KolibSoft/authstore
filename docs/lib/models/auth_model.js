import { CatalogueStatics } from "../modules/catalogue.js";
import { CredentialModel } from "./credential_model.js";
import { PermissionModel } from "./permission_model.js";

class AuthModel {

    /** @type {CredentialModel} */
    credential;

    /** @type {PermissionModel[]} */
    permissions;

    /** @type {string} */
    accessToken;

    /** @type {string} */
    refreshToken;

    constructor(json) {
        this.credential = json?.credential ?? new CredentialModel({ id: CatalogueStatics.NoGuid });
        this.permissions = json?.permissions ?? [];
        this.accessToken = json?.accessToken ?? "";
        this.refreshToken = json?.refreshToken ?? "";
    }

}

export {
    AuthModel
}