import HttClient from '../config/axios-base'
export default {
  get: () => HttClient.get('system'),
  post: (console) => HttClient.post('system', console),
};
