import { Link } from "react-router-dom";
import Stack from "@mui/material/Stack";
import { useAtom } from "jotai";
import { userAtom } from "@components/Provider/app";

export const Home = () => {
  const [user] = useAtom(userAtom);

  const handleLogout = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    sessionStorage.clear();
    window.location.href = "/api/Account/Logout";
  };

  return (
    <>
      <div className="flex min-h-full justify-center my-20 px-4 sm:px-6 lg:px-8">
        <div className="w-full max-w-md space-y-8">
          <div className="text-center">
            <h1 className="text-2xl">Velkommen</h1>
            <p>{user?.name}</p>
          </div>
          <div>
            <Link to="/bookings" style={{ textDecoration: "none" }}>
              <Stack direction="row" spacing={2}>
                <button
                  type="submit"
                  className="relative flex w-full justify-center rounded-md border border-transparent bg-green-600 py-2 px-4 text-sm font-medium text-white hover:bg-green-700"
                >
                  <span className="absolute inset-y-0 left-0 flex items-center pl-3"></span>
                  Se min bookingoversikt
                </button>
              </Stack>
            </Link>
          </div>
          <div>
            <Link to="/rooms" style={{ textDecoration: "none" }}>
              <Stack direction="row" spacing={2}>
                <button
                  type="submit"
                  className="relative flex w-full justify-center rounded-md border border-transparent bg-green-600 py-2 px-4 text-sm font-medium text-white hover:bg-green-700"
                >
                  <span className="absolute inset-y-0 left-0 flex items-center pl-3"></span>
                  Send en ny booking
                </button>
              </Stack>
            </Link>
          </div>
          <div>
            <div className="flex m-8">
              <button type="button" 
                className="relative flex w-full justify-center rounded-md border border-transparent bg-[#DF0D0D] py-2 px-4 text-sm font-medium text-white hover:bg-opacity-70" onClick={handleLogout}>
                Logg ut
              </button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};
