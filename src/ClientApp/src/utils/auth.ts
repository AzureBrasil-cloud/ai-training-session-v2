export const auth = {
  userIsLoggedIn: () => {
    let loggedUser = sessionStorage.getItem("loggedUser");
    loggedUser = loggedUser ? JSON.parse(loggedUser) : null;

    return loggedUser !== null
      && loggedUser !== undefined
      && loggedUser.hasOwnProperty("email")
      && loggedUser.hasOwnProperty("role");
  },
  userIsAdmin: () => {
    const loggedUserJson = sessionStorage.getItem("loggedUser");
    const loggedUser = loggedUserJson ? JSON.parse(loggedUserJson) as { email: string; role: string }  : null;

    return loggedUser !== null
      && loggedUser !== undefined
      && loggedUser.hasOwnProperty("email")
      && loggedUser.hasOwnProperty("role")
      && loggedUser.role.toLowerCase() === "admin";
  },
}
