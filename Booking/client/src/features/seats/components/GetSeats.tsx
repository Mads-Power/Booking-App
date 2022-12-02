import React, { useEffect, useState } from 'react';
import GetSeat from './GetSeat';
import { useQuery } from '@tanstack/react-query';
import { getSeats } from '../api/getSeats';

export const GetSeats = () => {
  const { isLoading, isError, data, error } = useQuery({
    queryKey: ['room'],
    queryFn: () => getSeats(),
  });

  if (isLoading) return <div>Laster inn rooms</div>;

  if (isError) return <div>Noe gikk galt</div>;

  return (
    <div>
      {data.map((item: any) => (
        <GetSeat key={item.id} {...item} />
      ))}
    </div>
  );
};
