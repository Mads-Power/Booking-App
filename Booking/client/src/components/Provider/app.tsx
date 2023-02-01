import React, { Suspense, useEffect } from 'react';
import { ErrorBoundary } from 'react-error-boundary';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import CircularProgress from '@mui/material/CircularProgress';
import { Button } from '@mui/material';
import { BrowserRouter, useNavigate } from 'react-router-dom';
import { atom, Provider, useAtom } from 'jotai';
import { User } from '@type/user';

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

export const dateAtom = atom<Date>(new Date(new Date().setHours(0,0,0,0)));

export const userAtom = atom<User | undefined>(undefined);


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
