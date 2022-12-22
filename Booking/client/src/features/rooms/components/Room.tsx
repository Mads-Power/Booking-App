import { CircularProgress, Button, Container, Grid } from "@mui/material";
import { useRooms } from "../api/getRooms";
import { Box } from "@mui/system";
import { Link } from "react-router-dom";
import { Seat } from "../../seats/types";

const Room = () => {
  const { isLoading, data, error } = useRooms();

  const Rooms = () => (
    <>
      {data?.map((rooms) => (
        <div key={rooms.id}>
          <Container>
            <h1>{rooms.name}</h1>
            <p>Capacity: {rooms.capacity}</p>
          </Container>
          <Container
            sx={{
              display: "flex",
              flexWrap: "wrap",
              backgroundColor: "#CECECE",
              borderRadius: "25px",
              flexDirection: "column-reverse",
              p: 1,
              border: "1px solid",
            }}
          >
            <Box
              sx={{
                display: "flex",
                flexWrap: "wrap",
                m: 2,
                gap: 2,
              }}
            >
              {rooms.seats.map((seatButton) => (
                <Link to={`/seatLayout/${seatButton.id}`} relative="path">
                  <Button key={seatButton.id}
                    variant="contained"
                    sx={{ backgroundColor: "#54A4D1" }}
                  >
                    {seatButton.id}
                  </Button>
                </Link>
              ))}
            </Box>
          </Container>
        </div>
      ))}
    </>
  );

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  return (
    <>
      <Rooms />
    </>
  );
};

export default Room;
