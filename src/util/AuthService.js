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
};
export default AuthService;
