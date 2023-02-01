import { useIsAuthenticated } from "@api/useIsAuthenticated"
import { userAtom } from "@components/Provider/app";
import { CircularProgress } from "@mui/material";
import { User } from "@type/user";
import { useAtom } from "jotai";
import { useNavigate } from "react-router-dom";

export const IsAuthenticated = ()=> {

  const [user, setUser] = useAtom(userAtom)
  const navigate = useNavigate();

  const { data, isSuccess, isFetched, error, isError } = useIsAuthenticated();

  if(isError) {
    console.log(isFetched)
    navigate('/login')
  }

  if (isFetched) {
    if (isSuccess) {
      // setUser(data as User);
      window.sessionStorage.setItem('user', JSON.stringify(data));
      navigate('/home')
    } else {
      console.error('Kunne ikke autentisere brukeren')
      navigate('/login')
    }
  }

  // First, we get the (possibly) authenticated user
  // If they're not authenticated, redirect them back to the login screen
  // If they're authenticated, redirect them to the home screen

  return (
    <div className="flex flex-col">
      <CircularProgress size={100} style={{ margin: '10vh auto' }} />
      <p className="text-lg text-center">Loading...</p>
    </div>
  )
}