import HttClient from '../axios-base'
export default {
  new: (userInfo) => HttClient.post('auth/register', userInfo),
};