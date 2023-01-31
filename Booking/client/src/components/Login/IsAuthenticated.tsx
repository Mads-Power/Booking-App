import { useIsAuthenticated } from "@api/useIsAuthenticated"
import { userAtom } from "@components/Provider/app";
import { User } from "@type/user";
import { useAtom } from "jotai";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export const IsAuthenticated = () => {

  const [user, setUser] = useAtom(userAtom)
  const { data, isSuccess, isFetched } = useIsAuthenticated();
  const navigate = useNavigate();

  useEffect(() => {
    if(isFetched) {
      if(isSuccess) {
        setUser(data as User);
        window.sessionStorage.setItem('user', JSON.stringify(data));
        navigate('/home')
      }
    }
  }, [data]);
  
  return(
    <>
    </>
  )
}