import React, { Suspense } from 'react';
import { ErrorBoundary } from 'react-error-boundary';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import CircularProgress from '@mui/material/CircularProgress';
import { Button } from '@mui/material';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'jotai';

const ErrorFallback = () => {
  return (
    <div role='alert'>
      <h2 className='text-lg font-semibold'>Ooops, something went wrong :( </h2>
      <Button onClick={() => window.location.assign(window.location.origin)}>
        Refresh
      </Button>
      )
    </div>
  );
};

const queryClient = new QueryClient();

type AppProviderProps = {
  children: React.ReactNode;
};

export const AppProvider = ({ children }: AppProviderProps) => {
  return (
    <Suspense
      fallback={
        <div>
          <CircularProgress />
        </div>
      }>
      <ErrorBoundary FallbackComponent={ErrorFallback}>
        <QueryClientProvider client={queryClient}>
          <Provider>
            <BrowserRouter>{children}</BrowserRouter>
          </Provider>
        </QueryClientProvider>
      </ErrorBoundary>
    </Suspense>
  );
};
