import { useIsAuthenticated } from "@api/useIsAuthenticated"

export const IsAuthenticated = () => {

  const { data } = useIsAuthenticated();

  if (data) console.log("SUCCESS");
  

  return(
    <>
    </>
  )
}