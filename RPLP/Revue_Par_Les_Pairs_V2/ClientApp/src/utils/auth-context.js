import { useContext, createContext } from "react";

export const ContexteAuth = createContext();

export function UtiliseAuth() {
  return useContext(ContexteAuth);
}
