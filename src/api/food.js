import HttClient from '../config/axios-base'
export default {
  get: () => HttClient.get('food'),
  new: (food) => HttClient.post('food', food),
  remove: (id) => HttClient.delete(`food/${id}`),
  update: (id, food) => HttClient.put(`food/${id}`, food),
};
