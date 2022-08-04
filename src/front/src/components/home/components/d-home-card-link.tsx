import React from 'react'
import { createStyles, Image, Card } from '@mantine/core'

export interface DHomeCardLinkProps {
  label: string
  icon: string
  caption: string
  link: string
}

const useStyles = createStyles(() => ({
  container: {
    background: '#dce1ff',
    transition: 'transform 0.250s',
    backfaceVisibility: 'hidden',
    transform: 'translateZ(0) perspective(1px)',
    cursor: 'pointer',

    '&:hover': {
      transform: 'scale(1.03)'
    }
  }
}))

const DHomeCardLink = (props: DHomeCardLinkProps) => {
  const {classes} = useStyles()

  const openLink = () => {
    // window.open(props.link, '_blank')
  }

  return (
    <Card onClick={openLink} className={`${classes.container} flex flex-nowrap gap-3`}>
      <div>
        <Image src={props.icon}/>
      </div>
      <div>
        <div className={'text-sm font-bold'}>{props.label}</div>
        <div className={'text-xs'}>{props.caption}</div>
      </div>
    </Card>
  )
}

export default DHomeCardLink
