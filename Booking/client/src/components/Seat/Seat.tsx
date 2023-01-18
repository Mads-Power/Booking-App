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
import { ColorDescription } from '@components/ColorDescription/colorDescription';
import { Divider } from '@mui/material'

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

  const initialSeatState = () => {
    let initialSeatState = "";
    data?.bookings.some(booking => {
      const dateIsoString = new Date(booking.date).toISOString();
      if(dateIsoString === date.toISOString()) {
        if(booking.userId === userData?.id) {
          initialSeatState = 'removeBookedSeat';
          return true;
        } else if(booking.userId !== userData?.id) {
          initialSeatState = 'bookedSeat';
          return true;
        }
      }
    });
    if(!initialSeatState.length) {
      initialSeatState = 'bookAvailableSeat';
    }
    return initialSeatState;
  }

  const [seatInfo, setSeatInfo] = useState(initialSeatState);

  useEffect(() => {
    if(!initialSeatState.length) {
      setSeatInfo(initialSeatState());
    }
    
  }, [ data ]);

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
        <div className='flex flex-col gap-y-8'>
          <div className='p-3 mb-4'>
            <ArrowCircleLeftIcon
              htmlColor='#DF8B0D'
              onClick={() => navigate(`/`)}
            />
          </div>
          <div className='flex flex-col gap-y-4 w-[90%] mx-auto'>
            <div className='m-3'>
              <ColorDescription />
            </div>
            <div>
              <DateSeat
                data={data!}
                userData={userData!}
                onSeatInfoChange={setSeatInfo}
              />
            </div>
            <Divider sx={{ mx:2, background:"#BDBDBD"}}/>
            <div className='flex flex-col w-full'>
            <BookSeat
          seat={data!}
          date={dayDate}
          data={userData!}
          seatInfo={seatInfo}
          onSeatInfoChange={setSeatInfo}
          />
            </div>
          </div>
        </div>
      </ThemeProvider>
    </>
  );
};
