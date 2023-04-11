import HttClient from './axios-base'
export default {
  get: () => HttClient.get('bill'),
  getFilter: (url) => HttClient.get(url),
  new: (systemId, rateId, startInfo) => HttClient.post(`bill/${systemId}/${rateId}/0`, startInfo),
  setFood: (id, setfoods) => HttClient.put(`bill/${id}/set-foods`, setfoods),
  close: (id, finishInfo) => HttClient.put(`bill/${id}/close`, finishInfo),
  remove: (id) => HttClient.delete(`bill/${id}`),
  changeMoney: (id, money) => HttClient.put(`bill/${id}/change-rate`, money),
  paymentMethod: (billId, paymentMethod) => HttClient.put(`bill/${billId}/change-payment`, paymentMethod),
};