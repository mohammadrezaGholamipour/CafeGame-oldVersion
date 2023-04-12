import AuthService from '@/util/AuthService'
import axios from 'axios';
// //////////////////////////////////
const HttClient = axios.create({
  baseURL: 'https://cafegameapi.iran.liara.run/api/',
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json;charset=UTF-8',
    Accept: 'application/json',
    'Access-Control-Allow-Origin': '*',
  },
});
HttClient.interceptors.request.use(
  (config) => {
    config.headers['Content-Type'] = 'application/json;charset=UTF-8';
    config.headers['X-Response-Delay-In-Seconds'] = "0";
    config.headers['authorization'] = `Bearer ${AuthService.getTokenUser()}`;
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);
HttClient.interceptors.response.use(
  (response) => {
    if (
      response.headers['content-type'] ===
      'application/octet-stream;charset=UTF-8'
    ) {
      return response.data || null;
    }
    return response.data || null;
  },
  (error) => {
    if (error && error.response) {
      switch (error.response.status) {
        case 400:
          break;
        case 401:
          AuthService.removeTokenUser();
          AuthService.removeState();
          window.location.href = '/login';
          break;
        case 403:
          break;
        case 404:
          break;
        case 405:
          break;
        case 408:
          break;
        case 411:
          break;
        case 413:
          break;
        case 414:
          break;
        case 415:
          break;
        case 500:
          break;
        case 501:
          break;
        case 502:
          break;
        case 503:
          break;
        case 504:
          break;
        case 505:
          break;
        default:
          console.log(`http client status : ${error.response.status}`);
      }
    }
    return Promise.reject(error);
  }
);
export default HttClient;
