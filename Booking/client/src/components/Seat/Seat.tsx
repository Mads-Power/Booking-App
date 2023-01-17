import { useEffect, useState } from 'react';
import { useSeatQuery } from '@api/useSeatQuery';
import { CircularProgress } from '@mui/material';
import { useParams, useNavigate } from 'react-router-dom';
import {
  nbNO,
} from '@mui/x-date-pickers';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import dayjs from 'dayjs';
import 'dayjs/locale/nb';
import { nbNO as coreNbNO } from '@mui/material/locale';
import { BookSeat } from './BookSeat';
import { DateSeat } from './DateSeat';
import ArrowCircleLeftIcon from '@mui/icons-material/ArrowCircleLeft';
import { useAtom } from 'jotai';
import { dateAtom } from '../../jotaiProvider';
import { useUserQuery } from '@api/useUserQuery';

const theme = createTheme(
  {
  },
  nbNO, // x-date-pickers translations
  coreNbNO // core translations
);

export const Seat = () => {
  const { seatId } = useParams();
  const { isLoading, data, error } = useSeatQuery(seatId!);
  const { data: userData } = useUserQuery('5');
  const [date] = useAtom(dateAtom);
  const navigate = useNavigate();
  const [seatInfo, setSeatInfo] = useState('bookAvailableSeat');

  if (isLoading) {
    return (
      <div style={{ display: 'flex' }}>
        <CircularProgress size={100} style={{ margin: '10vh auto' }} />
      </div>
    );
  }

  if (error) {
    return <h4>Kan ikke hente setet.</h4>;
  }


  const dayDate = dayjs(date);
  return (
    <>
      <ThemeProvider theme={theme}>
        <ArrowCircleLeftIcon htmlColor='#DF8B0D' onClick={() => navigate(`/`)} />
        <div
          style={{
            display: 'flex',
            flexDirection: 'column',
            overflow: 'hidden',
            margin: '10px',
            minWidth: '80vw',
          }}>
          <BookSeat
            seat={data!}
            date={dayDate}
            data={userData!}
            seatInfo={seatInfo}
            onSeatInfoChange={setSeatInfo}
          />
        </div>
        <DateSeat
          data={data!}
          userData={userData!}
          onSeatInfoChange={setSeatInfo}
        />
      </ThemeProvider>
    </>
  );
};
