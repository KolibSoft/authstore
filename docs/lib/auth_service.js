import { AuthConnector } from "./abstractions/auth_connector.js";
import { Catalogue } from "./modules/catalogue.js";


/**
 * @implements {AuthConnector}
 */
class AuthService {

    /** @type {(input: RequestInfo | URL, init?: RequestInit | undefined) => Promise<Response>} */
    fetch;

    /** @type {string} */
    uri;

    /** @returns {boolean} */
    get available() { return navigator.onLine; }

    /**
     * @param {LoginModel} login 
     * @returns {Promise<Catalogue.Result<AuthModel>>}
     */
    async accesAsync(login) {
        let uri = `${this.uri}`;
        let response = await fetch(uri, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(login)
        });
        let result = await Catalogue.ResultUtils.handleResult(response);
        return result;
    }

    /**
     * @param {string} id 
     * @param {string} refreshToken 
     * @returns {Promise<Catalogue.Result<AuthModel>>}
     */
    async refreshAsync(id, refreshToken) {
        let uri = `${this.uri}/${id}`;
        let response = await fetch(uri, {
            method: "GET",
            headers: {
                "Authorization": `bearer ${refreshToken}`
            }
        });
        let result = await Catalogue.ResultUtils.handleResult(response);
        return result;
    }

    /**
     * @param {{(input: RequestInfo | URL, init?: RequestInit | undefined) => Promise<Response>}} fetch 
     * @param {string} uri 
     */
    constructor(fetch, uri) {
        this.fetch = fetch;
        this.uri = uri;
    }

}

export {
    AuthService
}