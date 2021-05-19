export const config = {
  
    auth: {
        clientId: "80a02fd4-6df4-413f-9342-7b4894388536",
        authority: "https://login.microsoftonline.com/3055536a-130a-492f-8a62-391bdb79c9e9",
        redirectUri:"https://localhost:5001/signin-oidc",
        postLogoutRedirectUri: "http://localhost:5001/logout",
        validateAuthority: false
      },
      cache: {
        cacheLocation: "sessionStorage",
        storeAuthStateInCookie: true,
      },
    
};