import HttClient from './axios-base'
export default {
  get: () => HttClient.get('hourRate'),
  new: (money) => HttClient.post('hourRate', money),
  remove: (id) => HttClient.delete(`hourRate/${id}`),
};
