export const preOrder = {
  transformPreOrderKeyValuePairs: (keyValuePairs: string[]) => {
    return keyValuePairs.reduce((acc, pair) => {
      let [key, value] = pair.split(': ');

      key = key ? key.replace(/:/gi, "").trim() : "";
      value = value ? value.replace(/:/gi, "").trim() : "";

      console.log("Key:", key);
      console.log("Value:", value);

      if (key && value.toLowerCase() === "selected") {
        return `${acc}, ${key}`;
      }
      return acc;
    }, "").substring(2);
  }
}
