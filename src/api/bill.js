import HttClient from '../config/axios-base'
export default {
  get: () => HttClient.get('bill'),
  new: (systemId, rateId, startInfo) => HttClient.post(`bill/${systemId}/${rateId}`, startInfo),
  setFood: (id, setfoods) => HttClient.put(`bill/${id}/SetFoods`, setfoods),
  close: (id, finishInfo) => HttClient.put(`bill/${id}/Close`, finishInfo),
  remove: (id) => HttClient.delete(`bill/${id}`),
};