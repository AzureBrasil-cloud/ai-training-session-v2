export const validators = {
  isValidEmail: (email: string): boolean => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  },
  isValidPassword: (password: string): boolean => {
    if (password.length <= 8) {
      return false;
    }
    return true;
  }
}
