import { AuthService, CredentialService, CredentialModel, PermissionModel, CredentialDatabaseCatalogue, CredentialPermissionDatabaseCatalogue, CredentialPermissionModel, PermissionDatabaseCatalogue } from "../lib/main.js";
import { DatabaseCatalogue, DbContext, ServiceCatalogue } from "../lib/modules/catalogue.js";

class AuthStoreContext extends DbContext {

    onUpgrade(database) {
        IDBUtils.buildAuthStore(database);
    }

    constructor() {
        super("authstore", 1);
    }

}

class AuthStoreClient {

    async sync() {
        await this.credentials.sync();
        await this.permissions.sync();
        await this.credentialPermissions.sync();
    }

    constructor(fetch, uri, context, changes) {
        this.fetch = fetch;
        this.uri = uri;
        this.context = context;
        this.changes = changes;
        //
        this.auth = new AuthService(fetch, `${uri}/auth`);
        this.credentials = new ServiceCatalogue(
            new CredentialDatabaseCatalogue(x => new CredentialModel(x), context, "credential"),
            new CredentialService(x => new CredentialModel(x), fetch, `${uri}/credential`),
            changes.credentials
        );
        this.permissions = new ServiceCatalogue(
            new PermissionDatabaseCatalogue(x => new PermissionModel(x), context, "permission"),
            new CredentialService(x => new PermissionModel(x), fetch, `${uri}/permission`),
            changes.permissions
        );
        this.credentialPermissions = new ServiceCatalogue(
            new CredentialPermissionDatabaseCatalogue(x => new CredentialPermissionModel(x), context, "credential-permission"),
            new CredentialService(x => new CredentialPermissionModel(x), fetch, `${uri}/credential_permission`),
            changes.credentialPermissions
        );
    }

}

export {
    AuthStoreContext,
    AuthStoreClient
}