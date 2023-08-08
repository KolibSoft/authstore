class CredentialPermissionModel {

    /** @type {string} */
    id;

    /** @type {string} */
    credentialId;

    /** @type {string} */
    permissionId;

    /** @type {boolean} */
    active;

    /** @type {Date} */
    updatedAt;

    constructor(json) {
        this.id = json?.id ?? crypto.randomUUID();
        this.credentialId = json?.credentialId ?? "00000000-0000-0000-0000-000000000000";
        this.permissionId = json?.permissionId ?? "00000000-0000-0000-0000-000000000000";
        this.active = json?.active ?? true;
        this.updatedAt = new Date(json?.updatedAt ?? new Date());
    }

}

export {
    CredentialPermissionModel
}