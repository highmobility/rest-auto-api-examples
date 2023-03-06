import jwt
import uuid
import time
import requests

REST_API_CONFIG =  {
  "version": "3.0",
  "type": "rest_api",
  "private_key_id": "e6881b32-dec6-4d56-8bff-c8462eb070d6",
  "private_key": "-----BEGIN PRIVATE KEY-----\nMIGHAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBG0wawIBAQQgf98dg89RnunSQlEi\n9IqlSkvmtnb9uqU+wIez0cQTkYyhRANCAARv1elcg9Q/4H+e6AgXob59MN4n30Lr\nE7au3oKP4ginfkWjhrJX5ihXduqGyiUIezMKe5oQCMw4BLyqlQn+1oDX\n-----END PRIVATE KEY-----\n\n",
  "app_uri": "https://rest-api.high-mobility.com/v5",
  "app_id": "F2F7C9D6A1C409AEF4FCE226",
  "client_serial_number": "2BA0AFB864121DF62D"
}

payload = {
    'ver': REST_API_CONFIG['version'],
    'iss': REST_API_CONFIG['client_serial_number'],
    'aud': REST_API_CONFIG['app_uri'],
    'iat': int(time.time()),
    'jti': str(uuid.uuid4()),
    'sub': 'b55af95a-607c-47e8-ad37-5016a8beda61'
}

private_key = REST_API_CONFIG['private_key']
jwt_token = jwt.encode(payload, private_key, algorithm='ES256')

headers = {'Authorization': f'Bearer {jwt_token.decode("utf-8")}'}

url = f'{REST_API_CONFIG["app_uri"]}/vehicle_status'
response = requests.get(url, headers=headers)

print('Status code:', response.status_code)
print('Response body:', response.text)
