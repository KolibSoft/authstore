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
    rerfreshToken;

    constructor(json) {
        this.credential = json?.credential ?? new CredentialModel({ id: "00000000-0000-0000-0000-000000000000" });
        this.permissions = json?.permissions ?? [];
        this.accessToken = json?.accessToken ?? "";
        this.rerfreshToken = json?.rerfreshToken ?? "";
    }

}

export {
    AuthModel
}