const AuthService = {
  ///////////////////////////////////////////////
  getTokenUser: () => {
    return window.localStorage.getItem("tokenUser");
  },
  ///////////////////////////////////////////////
  setTokenUser: (token) => {
    window.localStorage.setItem("tokenUser", token);
  },
  ///////////////////////////////////////////////
  setAlarm: (alarm) => {
    window.localStorage.setItem("alarmList", alarm);
  },
  ///////////////////////////////////////////////
  getAlarm: () => {
    return localStorage.getItem("alarmList");
  },
  ///////////////////////////////////////////////
  setState: (state) => {
    window.localStorage.setItem("state", state);
  },
  getState: () => {
    return window.localStorage.getItem("state");
  },
  ///////////////////////////////////////////////
  removeTokenUser: () => {
    window.localStorage.removeItem('tokenUser');
  },
  ///////////////////////////////////////////////
  removeState: () => {
    window.localStorage.removeItem('state');
  },
  ///////////////////////////////////////////////
  removeAlarm: () => {
    window.localStorage.removeItem('alarmList');
  },
};
export default AuthService;
