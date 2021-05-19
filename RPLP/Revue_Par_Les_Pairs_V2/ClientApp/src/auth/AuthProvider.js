import React, { useEffect, useState } from "react";
import { config } from "./config";
import { UserAgentApplication } from "msal";
import { ContexteAuth } from "../utils/auth-context";
import { scopes } from "./scopes";

function AuthContext(props) {
  const userAgentApplication = new UserAgentApplication(config);

  const [loginState, setLogin] = useState({
    isAuthenticated: false,
    user: {},
    error: null,
  });

  // UseEffect qui permet d'aller valider le useState
  useEffect(() => {
    let user = userAgentApplication.getAccount();
    if (user) {
      getUserProfile();
    }
  }, []);

  //sore le login context dans une function avec useContext
  const login = async () => {
    try {
      await userAgentApplication.loginPopup({
        scopes: scopes,
        prompt: "select_account",
      });
      getUserProfile();
    } catch (err) {
      var error = {
        message: err.message,
        debug: JSON.stringify(err),
      };

      setLogin({
        isAuthenticated: false,
        user: {},
        error: error,
      });
    }
  };

  const logout = () => {
    userAgentApplication.logout();
  };

  //Aller chercher le userprofil et le token
  const getUserProfile = () => {
    try {
      var user = userAgentApplication.getAccount();
      setLogin({
        isAuthenticated: true,
        user: {
          displayName: user.name || user.userPrincipalName,
          email: user.userName,
          roles: user.idTokenClaims.roles,
        },
        error: null,
      });
    } catch (err) {
      var error = {};
      console.log(err);
      error = {
        message: err.message,
        debug: JSON.stringify(err),
      };

      setLogin({
        isAuthenticated: false,
        user: {},
        error: error,
      });
    }
  };

  //La const qui sera passer a travers du provider
  const authContext = {
    isAuthenticated: loginState.isAuthenticated,
    user: loginState.user,
    onLogin: () => login(),
    onLogout: () => logout(),
  };

  return (
    <ContexteAuth.Provider value={authContext}>
      {props.children}
    </ContexteAuth.Provider>
  );
}

export default AuthContext;
