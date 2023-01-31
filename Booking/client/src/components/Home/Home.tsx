import { Link } from "react-router-dom";
import Stack from "@mui/material/Stack";



export const Home = () => {
    return (
        <>
          <div className="flex min-h-full items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
            <div className="w-full max-w-md space-y-8">
              <div className="text-center">
                <h1 className="text-2xl">Velkommen</h1>
                <p>Vennligst logg inn for å kunne booke pulter</p>
              </div>
    
              <div>
                <Link to="/bookings" style={{ textDecoration: "none" }}>
                  <Stack direction="row" spacing={2}>
                    <button
                      type="submit"
                      className="relative flex w-full justify-center rounded-md border border-transparent bg-green-600 py-2 px-4 text-sm font-medium text-white hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
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
                      className="relative flex w-full justify-center rounded-md border border-transparent bg-green-600 py-2 px-4 text-sm font-medium text-white hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
                    >
                      <span className="absolute inset-y-0 left-0 flex items-center pl-3"></span>
                      Send en ny booking
                    </button>
                  </Stack>
                </Link>
              </div>
            </div>
          </div>
        </>
      );
}