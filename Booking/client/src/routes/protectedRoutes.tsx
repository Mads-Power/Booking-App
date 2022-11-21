import React, { Suspense } from 'react';
import MainLayout from '../components/Layout/MainLayout';

const protectedRoutes = () => {
  return (
    <Suspense fallback={<div>Loading...</div>}>
      {' '}
      <MainLayout />
    </Suspense>
  );
};

//TODO:
//set paths & elements

export default protectedRoutes;
