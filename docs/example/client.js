import { AuthService } from "../lib/auth_service.js";
import { CredentialService } from "../lib/credential_service.js";
import { Catalogue } from "../lib/modules/catalogue.js";

class AuthStoreContext extends Catalogue.DbContext {

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
        this.credentials = new Catalogue.ServiceCatalogue(
            new Catalogue.DatabaseCatalogue(context, "credential"),
            new CredentialService(fetch, `${uri}/credential`),
            changes.credentials
        );
        this.permissions = new Catalogue.ServiceCatalogue(
            new Catalogue.DatabaseCatalogue(context, "permission"),
            new CredentialService(fetch, `${uri}/permission`),
            changes.permissions
        );
        this.credentialPermissions = new Catalogue.ServiceCatalogue(
            new Catalogue.DatabaseCatalogue(context, "credential-permission"),
            new CredentialService(fetch, `${uri}/credential_permission`),
            changes.credentialPermissions
        );
    }

}

export {
    AuthStoreContext,
    AuthStoreClient
}