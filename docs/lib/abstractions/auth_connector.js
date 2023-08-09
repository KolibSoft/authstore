import { LoginModel } from "../models/login_model.js";
import { AuthModel } from "../models/auth_model.js";
import { Result } from "../modules/catalogue.js";

/**
 * @interface
 */
class AuthConnector {

    /** @returns {boolean} */
    get available() { return new Error("Not implemented"); }

    /**
     * @param {LoginModel} login 
     * @returns {Promise<Result<AuthModel>>}
     */
    accesAsync(login) { return new Error("Not implemented"); }

    /**
     * @param {string} id 
     * @param {string} refreshToken 
     * @returns {Promise<Result<AuthModel>>}
     */
    refreshAsync(id, refreshToken) { return new Error("Not implemented"); }

}

export {
    AuthConnector
}