import HttClient from '../config/axios-base'
export default {
  get: () => HttClient.get('bill'),
  new: (systemId, rateId, startInfo) => HttClient.post(`bill/${systemId}/${rateId}`, startInfo),
  setFood: (id, setfoods) => HttClient.put(`food/${id}/setfoods`, setfoods),
  close: (id, finishInfo) => HttClient.put(`food/${id}/close`, finishInfo),
  remove: (id) => HttClient.delete(`bill/${id}`),
};
