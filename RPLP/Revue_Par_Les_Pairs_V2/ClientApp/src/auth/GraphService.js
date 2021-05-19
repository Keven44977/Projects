

//Desuet
var graph = require('@microsoft/microsoft-graph-client');

function getAuthentificatedClient(accesToken){
    const client = graph.Client.init({
        authProvider: (done) =>{
            done(null, accesToken.accesToken);
        }
    });

    return client;
}

export async function getUserDetails(accesToken){
    const client = getAuthentificatedClient(accesToken);
    const user = await client.api('/me').get();
    return user;
}