import HttClient from '../axios-base'
export default {
  login: (userLogin) => HttClient.post('auth/login', userLogin),
};