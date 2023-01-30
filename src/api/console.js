import HttClient from '../config/axios-base'
export default {
  get: () => HttClient.get('system'),
  new: (console) => HttClient.post('system', console),
  remove: (id) => HttClient.delete(`system/${id}`),
};
