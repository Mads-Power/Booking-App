import React from "react";
import { Link } from "react-router-dom";
import { CircularProgress, Button, Container, Grid } from "@mui/material";
import Stack from "@mui/material/Stack";

const OfficeLayout = () => {
  return (
    <>
      <Container>
        <Link to="/mainLayout">
          <Stack direction="row" spacing={2}>
            <Button size="large">OSLO</Button>
            {/* <Button disabled>Disabled</Button>
          <Button href="#text-buttons">Link</Button> */}
          </Stack>
        </Link>
        <Button disabled size="large">
          DRAMMEN
        </Button>
      </Container>
    </>
  );
};

export default OfficeLayout;
