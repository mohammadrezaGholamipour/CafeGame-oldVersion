import HttClient from './axios-base'
export default {
  get: () => HttClient.get('bill'),
  getFilter: (url) => HttClient.get(url),
  new: (systemId, rateId, startInfo) => HttClient.post(`bill/${systemId}/${rateId}/0`, startInfo),
  setFood: (billId, foods) => HttClient.put(`bill/${billId}/set-foods`, foods),
  close: (billId, finishInfo) => HttClient.put(`bill/${billId}/close`, finishInfo),
  remove: (billId) => HttClient.delete(`bill/${billId}`),
  changeMoney: (billId, money) => HttClient.put(`bill/${billId}/change-rate`, money),
  paymentMethod: (billId, paymentMethod) => HttClient.put(`bill/${billId}/change-payment`, paymentMethod),
};