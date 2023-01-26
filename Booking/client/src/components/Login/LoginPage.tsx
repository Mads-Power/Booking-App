import { Link } from "react-router-dom";
import { Button, Container, Grid } from "@mui/material";
import Stack from "@mui/material/Stack";
import { useForm, SubmitHandler } from "react-hook-form";

type Inputs = {
  example: string;
  exampleRequired: string;
};

export const LoginPage = () => {
  const login = async (event: React.FormEvent) => {
    event.preventDefault();
    console.log("logged in");
  };

  return (
    <>
      <div className="flex  items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
        <div className="w-full max-w-md space-y-8">
          <div className="text-center">
            <h1 className="text-2xl">Velkommen</h1>
            <p>Vennligst logg inn for å kunne booke pulter</p>
          </div>
          <form className="mt-8 space-y-6" onSubmit={login}>
            <div className="flex min-h-full items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
              <button
                type="submit"
                className="group relative flex w-full justify-center rounded-md border border-transparent bg-green-600 py-2 px-4 text-sm font-medium text-white hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 "
              >
                <span className="absolute inset-y-0 left-0 flex items-center pl-3"></span>
                Sign in
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export const LoggedIn = () => {
  return (
    <>
      <div className="flex min-h-full items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
        <div className="w-full max-w-md space-y-8">
          <div className="text-center">
            <h1 className="text-2xl">Velkommen</h1>
            <p>Vennligst logg inn for å kunne booke pulter</p>
          </div>

          <div>
            <Link to="/" style={{ textDecoration: "none" }}>
              <Stack direction="row" spacing={2}>
                <button
                  type="submit"
                  className="group relative flex w-full justify-center rounded-md border border-transparent bg-green-600 py-2 px-4 text-sm font-medium text-white hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
                >
                  <span className="absolute inset-y-0 left-0 flex items-center pl-3"></span>
                  Se min bookingoversikt
                </button>
              </Stack>
            </Link>
          </div>
          <div>
            <Link to="/office" style={{ textDecoration: "none" }}>
              <Stack direction="row" spacing={2}>
                <button
                  type="submit"
                  className="group relative flex w-full justify-center rounded-md border border-transparent bg-green-600 py-2 px-4 text-sm font-medium text-white hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
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
};
