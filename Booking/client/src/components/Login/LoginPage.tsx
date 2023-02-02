export const LoginPage = () => {

  // Since we're using Azure AD for authentication, we need to redirect the user to a specific route.
  // On the callbakc the user will get redirected to one of our custom routes
  // Currently we're using the / (root) route for this and then we need some logic to determine where to navigate the user next
  const login = async (event: React.FormEvent) => {
    event.preventDefault();
    window.location.href = '/api/Account/Login';
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
                Logg in
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};