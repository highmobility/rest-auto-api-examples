// Replace the config with your snippet
var REST_API_CONFIG =  {
  "version": "1.2",
  "type": "rest_api",
  "private_key_id": "f46e9a6e-4dfc-4fc2-8d43-78bf33bfdf6d",
  "private_key": "-----BEGIN PRIVATE KEY-----\nMIGHAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBG0wawIBAQQgdyHevWjV81hMC3r8\njc50YoP6xIryDMeFTOEAuwOyzkihRANCAAQ8cx5XXnDgiLcrRxLHQQOX6L+1abEb\nSc+x/Si2hJp5Kp6ZHDOAcTF2ikSGsysxHrc0XG6nLPYS0UF15jieojHj\n-----END PRIVATE KEY-----\n\n",
  "app_uri": "https://sandbox.rest-api.high-mobility.com/v3",
  "app_id": "08F06CDB482DE3575278C31F"
}



var jwt = require('jsonwebtoken')
var uuid4 = require('uuid4');

var payload = {
  'api_version': REST_API_CONFIG.version,
  'app_id': REST_API_CONFIG.app_id,
  'aud': REST_API_CONFIG.app_uri,
  'iat': Math.round(Date.now()/1000),
  'jti': uuid4(),
  'access_token': '0c4c3cd1-fe5c-4b7a-85dd-cb86e548f29e'
};

var privateKey = Buffer.from(REST_API_CONFIG.private_key, 'utf8')
var jwtToken = jwt.sign(payload, privateKey, { algorithm: 'ES256' })

console.log({headers: {'Authorization': 'Bearer ' + jwtToken}})

var fs = require('fs')
var request = require('request');

request.get(
  REST_API_CONFIG.app_uri + '/vehicle_status',
  {headers: {'Authorization': 'Bearer ' + jwtToken}},
  function (error, response, body) {
    console.log('error:', error);
    console.log('statusCode:', response && response.statusCode);
    console.log('body:', body);
  }
)
