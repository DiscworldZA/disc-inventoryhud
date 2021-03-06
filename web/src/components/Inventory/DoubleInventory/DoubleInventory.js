import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/core';
import Inventory from '../Inventory';
import React from 'react';
import Hotbar from '../Hotbar/Hotbar';
import DescriptionBox from '../DescriptionBox/DescriptionBox';
import { useSelector } from 'react-redux';

const useStyles = makeStyles(theme => ({
  grid: {
    left: 0,
    right: 0,
    top: 0,
    bottom: 0,
    margin: 'auto',
    position: 'absolute',
    width: '100%',
    height: '100%',
  },
  inventory: {
    width: '100%',
    height: '100%',
  },
  gridItem: {
    height: '70%',
    maxHeight: '70%',
  },
  description: {
    height: '25vh',
    maxHeight: '25vh',
  }
}));

export default (props) => {
  const classes = useStyles();
  const inventory = useSelector(state => state.inventory.player);
  const hotbar = useSelector(state => state.inventory.hotbar);
  return (
    <Grid container justify={'flex-start'} alignItems={'flex-start'} spacing={3} className={classes.grid}>
      <Grid item xs={6} className={classes.inventory}>
        <Grid container justify={'flex-start'} alignItems={'flex-start'} spacing={3} className={classes.inventory}>
          <Grid item xs={12} className={classes.gridItem}>
            <Inventory data={inventory} hideUse hideDrop hideGive/>
          </Grid>
          <Grid item xs={12}>
            <Hotbar data={hotbar}/>
          </Grid>
        </Grid>
      </Grid>
      <Grid item xs={6} className={classes.inventory}>
        <Grid container justify={'flex-start'} alignItems={'flex-start'} spacing={3} className={classes.inventory}>
          <Grid item xs={12} className={classes.gridItem}>
            <Inventory data={props.inventory} hideUse hideDrop hideGive/>
          </Grid>
          <Grid item xs={12} className={classes.description}>
            <DescriptionBox/>
          </Grid>
        </Grid>
      </Grid>
    </Grid>
  );
}
